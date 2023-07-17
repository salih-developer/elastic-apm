import React, { Component } from 'react';
import { USERS_API_URL } from '../../constants';
import { Button } from 'reactstrap';
import UserTaskModal from './UserTaskModal';

import moment from 'moment';


export class UserTaskList extends Component {
    static displayName = UserTaskList.name;

  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };
  }

  async usersData() {
    const response = await fetch(`${USERS_API_URL}UserTasks`);
    const data = await response.json();
    this.setState({ users: data, loading: false });
  }
  updateState = () => {
    this.usersData();
  }
  componentDidMount() {
    this.usersData();
  }
  deleteItem = id => {
    let confirmDeletion = window.confirm('Do you really wish to delete it?');
    if (confirmDeletion) {
      fetch(`${USERS_API_URL}UserTasks/${id}`, {
        method: 'delete',
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .then(res => {
          this.usersData();
        })
        .catch(err => console.log(err));
    }
  }
   renderUsersTable(users) {
    return (
      <div>
        <div>
        <UserTaskModal    isNew={true} updateState={this.updateState} />
        </div>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Id</th>
              <th>User</th>
              <th>Subject</th>
              <th>Start Date</th>
              <th>Start Time</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {users.map(item =>
              <tr key={item.Id}>
                <td>{item.id}</td>
                <td>{item.user.email}</td>
                <td>{item.subject}</td>
                <td>{moment(item.startDate).format('MM/DD/YYYY')}</td>
                <td>{item.startTime}</td>
                <td>{item.userTaskStatus.name}</td>
                <td>
                <UserTaskModal
                  isNew={false}
                  task={item}
                  updateState={this.updateState}
                />
                 <Button color="danger" onClick={() => this.deleteItem(item.id)}>Delete</Button>
                </td>
                 
                 
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderUsersTable(this.state.users);

    return (
      <div>
        <h4 id="tabelLabel" >Tasks</h4>
        {contents}
      </div>
    );
  }


}
