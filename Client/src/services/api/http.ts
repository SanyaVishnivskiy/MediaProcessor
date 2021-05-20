import axios from "axios";

const baseUri: string = "https://localhost:9300/";

const get = async (uri: string) => {
    try {
        const res = await axios.get(baseUri + uri);
        return res;
    } catch (e) {
        console.log(e);
        throw new Error(e);
    }
}

const getDownloadLink = (uri: string): string => {
    return baseUri + uri;
}

const post = async (uri: string, body: any) => {
    try {
        const res = await axios.post(baseUri + uri, body);
        return res;
    } catch (e) {
        console.log(e);
        throw new Error(e);
    }
} 

const put = async(uri: string, body: any) => {
    try {
        const res = await axios.put(baseUri + uri, body);
        return res;
    } catch (e) {
        console.log(e);
        throw new Error(e);
    }
}

const httpDelete = async(uri: string) => {
    try {
        const res = await axios.delete(baseUri + uri);
        return res;
    } catch (e) {
        console.log(e);
        throw new Error(e);
    }
}

export {
    baseUri,
    get,
    post,
    put,
    getDownloadLink,
    httpDelete
};