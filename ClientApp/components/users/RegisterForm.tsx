import * as React from 'react'
import Input from '../common/Input'

const RegisterForm = (props: any) => (
  <div>
    <form onSubmit={props.onSubmit}>
      {props.error ? <h3>{props.error}</h3> : null}
      <div className='form-group'>
        <Input
          className='form-control'
          name='name'
          value={props.user.name}
          onChange={props.onChange} 
          label='User name' />
      </div>
      <div className='form-group'>
        <Input
          className='form-control'
          name='email'
          value={props.user.email}
          onChange={props.onChange} 
          label='E-mail' />
      </div>
      <div className='form-group'>
        <Input
          className='form-control'
          name='password'
          type='password'
          value={props.user.password}
          onChange={props.onChange}
          label='Password' />
      </div>
      <div className='form-group'>
        <Input
          className='form-control'
          type='password'
          name='confirmPassword'
          value={props.user.confirmPassword}
          onChange={props.onChange} 
          label='Confirm password' />
      </div>
      <button
        className='btn btn-default'>Register</button>
    </form>
  </div>
)

export default RegisterForm
