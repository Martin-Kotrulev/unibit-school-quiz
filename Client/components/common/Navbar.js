import React, { Component } from 'react'
import Auth from '../../Auth'
import { Navbar, Nav } from 'react-bootstrap'
import RouteNavItem from './RouteNavItem'
import BrandLink from './BrandLink'

export default class AppNavbar extends Component {
  render () {
    return (
      <Navbar>
        <Navbar.Header>
          <Navbar.Brand>
            <BrandLink to='/'>ReactSkeleton</BrandLink>
          </Navbar.Brand>
        </Navbar.Header>
        <Nav>
          <RouteNavItem to='/'>Home</RouteNavItem>
        </Nav>
        {Auth.isAuthenticated() ? (
          <Nav />
              ) : (
                null
              )}
        {Auth.isAuthenticated() ? (
          <Nav pullRight>
            <RouteNavItem to='/'><span className='glyphicon glyphicon-user' /> {Auth.getUser()}</RouteNavItem>
            <RouteNavItem to='/users/logout'><span className='glyphicon glyphicon-log-in' /> Logout</RouteNavItem>
          </Nav>
            ) : (
              <Nav pullRight>
                <RouteNavItem to='/users/login'><span className='glyphicon glyphicon-user' /> Login</RouteNavItem>
                <RouteNavItem to='/users/register'><span className='glyphicon glyphicon-log-in' /> Register</RouteNavItem>
              </Nav>
            )}
      </Navbar>
    )
  }
}
