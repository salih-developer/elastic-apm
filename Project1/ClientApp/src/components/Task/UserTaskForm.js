import React from 'react';
import { Button, Form, FormGroup, Input, Label, } from 'reactstrap';
import { USERS_API_URL } from '../../constants';
import Moment from 'moment';
import { ReactDatez, ReduxReactDatez } from 'react-datez';
import 'react-datez/dist/css/react-datez.css';
export default class UserTaskForm extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            users: [], loading: false, userTaskStatus: [],
            id: 0,
            description: '',
            endDate: null,
            endTime: null,
            startDate: null,
            startTime: null,
            subject: '',
            userId: 0,
            userTaskStatusId: 0
        };
    }

    async usersData() {
        const response = await fetch(`${USERS_API_URL}users`);
        const data = await response.json();
        this.setState({ users: data, loading: false });
    }
    async userTaskStatusData() {
        const response = await fetch(`${USERS_API_URL}UserTaskStatus`);
        const data = await response.json();
        this.setState({ userTaskStatus: data, loading: false });
    }

    componentDidMount() {
        if (this.props.task) {
            const { id, description, endDate, endTime, startDate, startTime, subject, userId, userTaskStatusId } = this.props.task
            this.setState({ id, description, endDate, endTime, startDate, startTime, subject, userId, userTaskStatusId });
        }
        this.usersData()
        this.userTaskStatusData();
    }

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }
    onChangestartDate= e => {
        this.setState({startDate:e});
    }
    onChangeendDate= e => {
        this.setState({endDate:e});
    }
    submitNew = e => {
        e.preventDefault();
        fetch(`${USERS_API_URL}UserTasks`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                userId: this.state.userId,
                userTaskStatusId: this.state.userTaskStatusId,
                subject: this.state.subject,
                description: this.state.description,
                startDate: this.state.startDate,
                startTime: this.state.startTime,
                endDate: this.state.endDate,
                endTime: this.state.endTime
            })
        })
            .then(user => {
                this.props.updateState();
                this.props.toggle();
            })
            .catch(err => console.log(err));
    }

    submitEdit = e => {
        e.preventDefault();
        fetch(`${USERS_API_URL}UserTasks/${this.state.id}`, {
            method: 'put',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: this.state.id,
                userId: this.state.userId,
                userTaskStatusId: this.state.userTaskStatusId,
                subject: this.state.subject,
                description: this.state.description,
                startDate: this.state.startDate,
                startTime: this.state.startTime,
                endDate: this.state.endDate,
                endTime: this.state.endTime
            })
        })
            .then(() => {
                this.props.toggle();
                this.props.updateState();
            })
            .catch(err => console.log(err));
    }

    render() {
        return <Form onSubmit={this.props.task ? this.submitEdit : this.submitNew}>
            <FormGroup>
                <Label for="userId">User:</Label>
                <select name="userId" class="form-control" onChange={this.onChange} required >
                    <option value="-1">Select</option>
                    {
                        this.state.users.map((option) => (
                            <option selected={option.id===this.state.id} value={option.id}>{option.email}</option>
                        ))
                    }
                </select>
            </FormGroup>
            <FormGroup>
                <Label for="userTaskStatusId">Status:</Label>
                <select name="userTaskStatusId" class="form-control" onChange={this.onChange} required  >
                    <option value="-1">Select</option>
                    {
                        this.state.userTaskStatus.map((option) => (
                            <option selected={option.id===this.state.id} value={option.id}>{option.name}</option>
                        ))
                    }
                </select>
            </FormGroup>
            <FormGroup>
                <Label for="subject">subject:</Label>
                <Input type="text" name="subject" onChange={this.onChange} value={this.state.subject === null ? '' : this.state.subject} required />
            </FormGroup>
            <FormGroup>
                <Label for="description">description:</Label>
                <Input type="textarea" name="description" onChange={this.onChange} value={this.state.description === null ? '' : this.state.description} />
            </FormGroup>
            <FormGroup>
                <Label for="startDate">startDate:</Label>
                <ReactDatez dateFormat="MM/DD/YYYY" name="startDate" handleChange={this.onChangestartDate} value={this.state.startDate} className="form-control" />
                {/* <Input type="date" format="YYYY-MM-DD" name="startDate" onChange={this.onChange} value={this.state.startDate === null ? '' : this.state.startDate} /> */}
            </FormGroup>
            <FormGroup>
                <Label for="startTime">startTime:</Label>
                <Input type="time" name="startTime" onChange={this.onChange} value={this.state.startTime === null ? '' : this.state.startTime} required />
            </FormGroup>
            <FormGroup>
                <Label for="endDate">endDate:</Label>
                <ReactDatez dateFormat="MM/DD/YYYY" name="endDate" handleChange={this.onChangeendDate} value={this.state.endDate} className="form-control" />
                {/* <Input type="date" name="endDate" onChange={this.onChange} value={this.state.endDate === null ? '' : this.state.endDate} /> */}
            </FormGroup>
            <FormGroup>
                <Label for="endTime">endTime:</Label>
                <Input type="time" name="endTime" onChange={this.onChange} value={this.state.endTime === null ? '' : this.state.endTime} />
            </FormGroup>
            <Button>Send</Button>
        </Form>;
    }
}

