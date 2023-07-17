import React from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';

import { USERS_API_URL } from '../../constants';

class UserForm extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            loading: false, userTaskStatus: [],
            id: 0,
            name: '',
            surname: '',
            email: '',
        };
    }
  

    componentDidMount() {
        if (this.props.user) {
            const { id, name, surname, email } = this.props.user
            this.setState({ id, name, surname, email });
        }
    }

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    submitNew = e => {
        e.preventDefault();
        fetch(`${USERS_API_URL}users`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: this.state.name,
                surname: this.state.surname,
                email: this.state.email,
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
        fetch(`${USERS_API_URL}users/${this.state.id}`, {
            method: 'put',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: this.state.id,
                name: this.state.name,
                surname: this.state.surname,
                email: this.state.email,

            })
        })
            .then(() => {
                this.props.toggle();
                this.props.updateState();
            })
            .catch(err => console.log(err));
    }

    render() {
        return <Form onSubmit={this.props.user ? this.submitEdit : this.submitNew}>
            <FormGroup>
                <Label for="email">Email:</Label>
                <Input type="email" name="email" onChange={this.onChange} value={this.state.email === null ? '' : this.state.email} required />
            </FormGroup>
            <FormGroup>
                <Label for="name">Name:</Label>
                <Input type="text" name="name" onChange={this.onChange} value={this.state.name === '' ? '' : this.state.name} />
            </FormGroup>
            <FormGroup>
                <Label for="surname">surname:</Label>
                <Input type="text" name="surname" onChange={this.onChange} value={this.state.surname === null ? '' : this.state.surname} />
            </FormGroup>
            <Button>Send</Button>
        </Form>;
    }
}

export default UserForm;