import axios, { AxiosError } from "axios";
import { ElMessage } from "element-plus"

const http = axios.create({
    headers: {
        "token": 'you token'
    },
    timeout: 1000 * 10
})

http.interceptors.request.use((config) => {
    //自定义拦截请求逻辑处理
    config.headers['X-Requested-With'] = 'XMLHttpRequest'
    return config;
})

http.interceptors.response.use((response) => {
    return response
}, (error: AxiosError) => {
    //自定义拦截响应处理，比如500状态的时候，提示错误等
    console.log(error);
    console.log(error.response?.data);
    if (error.response?.status == 500) {

        let data = error.response?.data;
        let message = "服务器繁忙"
        if (data.message) {
            message = data.message
        }
        ElMessage({
            showClose: false,
            message: message,
            type: 'error'
        })
        return Promise.reject(error)
    }
})

export {
    http
}