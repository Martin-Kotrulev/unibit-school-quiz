import React from 'react'
import Input from '../common/Input'
import { Form, Button, Col, Label } from 'react-bootstrap'

const RegisterForm = (props) => (
  <Col lg={6} lgOffset={3}>
    <Form onSubmit={props.onSubmit}>
      {props.error ? <h3><Label bsStyle='danger'>{props.error}</Label></h3> : null}
      <Input
        className='form-control'
        name='email'
        value={props.user.email}
        onChange={props.onChange}
        label='E-mail' />
      <Input
        className='form-control'
        name='password'
        type='password'
        value={props.user.password}
        onChange={props.onChange}
        label='Password' />
      <Input
        className='form-control'
        type='password'
        name='confirmPassword'
        value={props.user.confirmPassword}
        onChange={props.onChange}
        label='Confirm password' />
      <Button bsStyle='primary' type='submit'>Register</Button>
    </Form>
  </Col>
)

export default RegisterForm
