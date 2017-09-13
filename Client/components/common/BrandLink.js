import React from 'react'
import { Route, NavLink } from 'react-router-dom'

export default props => (
  <Route
    path={props.to}
    exact
    children={({ match, history }) => (
      <NavLink
        onClick={e => history.push(e.currentTarget.getAttribute('to'))}
        {...props} >
        {props.children}
      </NavLink>
    )}
  />
)
