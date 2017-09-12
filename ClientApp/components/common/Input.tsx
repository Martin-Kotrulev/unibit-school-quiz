import * as React from 'react'

interface InputProps 
{ 
  className: string,
  type: string,
  name: string,
  label: string,
  id: number,
  value: any,
  onChange: Function
}

const Input = (props: any) => {
  let type = props.type || 'text'

  return (
    <div className='container'>
      {props.label
         ? <label htmlFor={props.name} className='from-control'>
           {props.label}:
         </label>
         : null}
      <input
        className={props.className}
        type={type}
        name={props.name}
        placeholder={props.label}
        id={props.id}
        value={props.value}
        onChange={props.onChange} />
    </div>
  )
}

export default Input
