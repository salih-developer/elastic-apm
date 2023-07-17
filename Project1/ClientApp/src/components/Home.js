import React, { Component } from 'react';
import { USERS_API_URL } from '../constants';
import { Label } from 'reactstrap';
import { ListofToday } from './ListofToday';

export class Home extends Component {
  constructor(props) {
    super(props);
    this.state = { users: [], loading: true, childComponentRef: null };
  }
  getChildComponent = (childComponent) => {
    this.childComponentRef = childComponent;
  }

  async usersData() {
    const response = await fetch(`${USERS_API_URL}users`);
    const data = await response.json();
    this.setState({ users: data, loading: false });
  }

  onChange = e => {
    this.childComponentRef.sayHi(e.target.value);
  }
  componentDidMount() {
    this.usersData();
  }
  render() {
    return (
      <div>
        <br></br>
        <Label for="userId">User:</Label>
        <select name="userId" class="form-control" onChange={this.onChange} required >
          <option value="-1">Select</option>
          {
            this.state.users.map((option) => (
              <option selected={option.id === this.state.id} value={option.id}>{option.email}</option>
            ))
          }
        </select>
        <ListofToday getChildComponent={this.getChildComponent} >

        </ListofToday>
      </div>
    );
  }
}
