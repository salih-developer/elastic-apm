import React, { Component } from 'react';
import UserModal from './UserModal';
import { USERS_API_URL } from '../../constants';
import { Button } from 'reactstrap';

export class UserList extends Component {
  static displayName = UserList.name;

  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };
  }

  async usersData() {
    const response = await fetch(`${USERS_API_URL}users`);
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
      fetch(`${USERS_API_URL}users/${id}`, {
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
        <UserModal    isNew={true} updateState={this.updateState} />
        </div>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Id</th>
              <th>Email</th>
              <th>Name</th>
              <th>Surname</th>
            </tr>
          </thead>
          <tbody>
            {users.map(item =>
              <tr key={item.Id}>
                <td>{item.id}</td>
                <td>{item.email}</td>
                <td>{item.name}</td>
                <td>{item.surname}</td>
                <td>
                <UserModal
                  isNew={false}
                  user={item}
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
        <h4 id="tabelLabel" >Users</h4>
        {contents}
      </div>
    );
  }


}
