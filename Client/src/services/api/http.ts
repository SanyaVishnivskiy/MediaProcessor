import axios, { AxiosRequestConfig } from "axios";
import { Auth } from "../auth/auth";
import { Redirect } from "../navigation/redirect";

const baseUri: string = "https://localhost:9300/";

const auth = new Auth();

const getAuthHeaderValue = (): string => {
    const loginResult = auth.getToken();
    return `Bearer ${loginResult?.token}`;
}

axios.interceptors.request.use(function (config) {
    config.headers = {
        ...config.headers,
        Authorization: getAuthHeaderValue()
    };
    return config;
});

axios.interceptors.response.use(
    (value) => {
        return value;
    },
    (error) => {
        console.log("in interceptors", JSON.stringify(error, null, 2));
        if ((error?.response?.status ?? 0) === 401) {
            new Auth().deleteToken();
            Redirect.toLogin();
        }
        return Promise.reject(error.response);
    })

const get = async (uri: string) => {
    const res = await axios.get(baseUri + uri);
    return res;
}

const getDownloadLink = (uri: string): string => {
    return baseUri + uri;
}

const post = async (uri: string, body: any) => {
    const res = await axios.post(baseUri + uri, body);
    return res;
} 

const put = async(uri: string, body: any) => {
    const res = await axios.put(baseUri + uri, body);
    return res;
}

const httpDelete = async(uri: string) => {
    const res = await axios.delete(baseUri + uri);
    return res;
}

export {
    baseUri,
    get,
    post,
    put,
    getDownloadLink,
    httpDelete
};