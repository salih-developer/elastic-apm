import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import { UserList } from './components/User/UserList';
import { UserTaskList } from './components/Task/UserTaskList';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
            <Route path='/Users' component={UserList} />
            <Route path='/Tasks' component={UserTaskList} />
      </Layout>
    );
  }
}
