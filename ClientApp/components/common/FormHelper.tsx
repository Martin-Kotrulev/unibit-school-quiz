class FormHelper {
  static setState: any;
  static state: any;
  
  static handleFormChange(event: any, stateField: string) {
    const target = event.target
    const field = target.name
    const value = target.value

    const state = this.state[stateField]
    state[field] = value

    this.setState({ [stateField]: state })
  }
}

export default FormHelper