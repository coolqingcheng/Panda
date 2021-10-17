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
    // console.log(response.data)
    return response.data
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

    }
    if (error.response?.status == 401) {
        ElMessage({
            showClose: false,
            message: "访问需要登录",
            type: 'warning'
        })
    }
    if (error.response?.status == 404) {
        ElMessage({
            showClose: false,
            message: "Url地址错误！【404】",
            type: 'warning'
        })
    }
    return Promise.reject(error)
})

const get = async <T>(url: string, params: {}) => {
    var res = await http.get(url,{params:params})
    return res as any
}

export {
    http,
    get
}