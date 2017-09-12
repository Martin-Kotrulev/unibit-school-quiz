import * as React from 'react'
import { Route, Switch } from 'react-router-dom'
import Login from '../users/Login'
import Register from '../users/Register'
import PrivateRoute from './PrivateRoute'
import Logout from '../users/Logout'
import Statistics from './Statistics'
import { Layout } from './Layout'

const routes = () => (
  <Layout>
    <Route path='/users/login' component={Login} />
    <Route path='/users/register' component={Register} />
    <PrivateRoute path='/users/logout' component={Logout} />
  </Layout>
)

export default routes
