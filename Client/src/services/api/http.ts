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

const download = async (uri: string): Promise<Blob> => {
    try {
        const res = await axios({
            url: baseUri + uri, 
            method: 'GET',
            responseType: 'blob'
        });
        
        return new Blob([res.data]);;
    } catch (e) {
        console.log(e);
        throw new Error(e);
    }
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

export {
    baseUri,
    get,
    post,
    put,
    download
};