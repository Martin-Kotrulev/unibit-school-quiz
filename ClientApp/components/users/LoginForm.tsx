import * as React from 'react'
import Input from '../common/Input'

const LoginForm = (props: any) => (
  <div>
    <form onSubmit={props.onSubmit}>
      {props.error ? <h3>{props.error}</h3> : null}
      <div className='form-group'>
        <Input
          className='form-control'
          name='email'
          value={props.user.email}
          onChange={props.onChange} 
          label='User E-mail' />
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
      <button
        className='btn btn-default'>Login</button>
    </form>
  </div>
)

export default LoginForm
