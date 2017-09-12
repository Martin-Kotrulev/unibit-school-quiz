import axios from 'axios'
import Auth from './Auth'

const BASE_URL = 'http://localhost:5000/api'

interface AxiosOptions { headers: { [index: string]: string } }

class Http {
  static get (url: string, secured: boolean = false) {
    let axiosOptions: AxiosOptions = { headers: {} }

    if (secured) {
      axiosOptions.headers['Authorization'] = `bearer ${Auth.getToken()}`
    }

    return axios.get(`${BASE_URL}${url}`, axiosOptions)
      .then((res: any) => res.data)
  }

  static post (url: string, data: any, secured: boolean = false) {
    let axiosOptions: AxiosOptions = { headers: {} }

    if (secured) {
      axiosOptions.headers['Authorization'] = `bearer ${Auth.getToken()}`
    }

    return axios.post(`${BASE_URL}${url}`, data, axiosOptions)
      .then(res => res.data)
  }
}

export default Http
