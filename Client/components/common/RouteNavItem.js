import React from 'react'
import { Route } from 'react-router-dom'
import { NavItem } from 'react-bootstrap'

export default props => (
  <Route
    path={props.to}
    exact
    children={({ match, history }) => (
      <NavItem
        onClick={e => history.push(e.currentTarget.getAttribute('to'))}
        {...props} >
        {props.children}
      </NavItem>
    )}
  />
)
