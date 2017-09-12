import React from 'react'
import { Route, Switch } from 'react-router-dom'
import Statistics from './Statistics'
import Login from '../users/Login'
import Register from '../users/Register'
import PrivateRoute from './PrivateRoute'
import Logout from '../users/Logout'
import { Grid } from 'react-bootstrap'

const Routes = () => (
  <Grid fluid>
    <Switch>
      <Route path='/' exact component={Statistics} />
      <Route path='/users/login' component={Login} />
      <Route path='/users/register' component={Register} />
      <PrivateRoute path='/users/logout' component={Logout} />
    </Switch>
  </Grid>
)

export default Routes
