import React, { Component } from 'react';
import { USERS_API_URL } from '../constants';
import moment from 'moment';
export class ListofToday extends Component {
    constructor(props) {
        super(props);
        this.state = { tasks: [], loading: true };
        this.props.getChildComponent(this);
    }
    sayHi = (id) => {
        this.taskData(id);
    }

    async taskData(id) {
        const response = await fetch(`${USERS_API_URL}UserTasks/Listoftoday?id=${id}`);
        const data = await response.json();
        this.setState({ tasks: data, loading: false });
    }
     
    render() {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Start Date</th>
                            <th>Start Time</th>
                            <th>Tasks</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.tasks.map(item =>
                            <tr>
                                <td>{moment(item.startDate).format('MM/DD/YYYY')}</td>
                                <td>{item.startTime}</td>
                                <td style={{ }}>
                                    <ol>
                                    {item.datas.map(subitem=>
                                            <li>{subitem.subject} - {subitem.userTaskStatus.name} - {subitem.userTaskStatus.color}</li>
                                    )}
                                    </ol>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}
