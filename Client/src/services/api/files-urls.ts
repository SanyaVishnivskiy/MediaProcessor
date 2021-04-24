const getfileStorePrefix = (schema: string) => {
    let filesPrefix = "files/";
    if (schema == "local") {
        filesPrefix += "local/"
    }

    return filesPrefix;
}

export {
    getfileStorePrefix
}