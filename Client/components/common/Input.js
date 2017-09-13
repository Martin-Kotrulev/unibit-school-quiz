import React from 'react'
import { FormControl, FormGroup } from 'react-bootstrap'

const Input = (props) => {
  let type = props.type || 'text'

  return (
    <FormGroup>
      {props.label
      ? <label
        htmlFor={props.name}>{props.label}:</label>
      : null }
      <FormControl
        className={props.className}
        type={type}
        name={props.name}
        placeholder={props.label}
        id={props.id}
        value={props.value}
        onChange={props.onChange} />
    </FormGroup>
  )
}

export default Input
