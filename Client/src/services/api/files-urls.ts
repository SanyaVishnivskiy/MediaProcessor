const getfileStorePrefix = (schema: string) => {
    let filesPrefix = "files/";
    if (!schema || schema == "local") {
        filesPrefix += "local/"
    }

    return filesPrefix;
}

export {
    getfileStorePrefix
}