export default class FormHelper {
  static handleFormChange (event, stateField) {
    const target = event.target
    const field = target.name

    let value
    if (target.type === 'checkbox') {
      value = target.checked
    } else {
      value = target.value
    }

    const state = this.state[stateField]
    state[field] = value

    this.setState({ [stateField]: state })
  }
}
