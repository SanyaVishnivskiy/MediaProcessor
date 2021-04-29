export const objToQuery = (obj: any): string => {
    let str = [];
    for (var p in obj)
      if (obj.hasOwnProperty(p)) {
        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
      }
    return str.join("&");
}

export const queryToObj = (search: string): URLSearchParams => {
  const query = search.charAt(0) === '?'
    ? search.substring(1)
    : search;

  const path = new URLSearchParams(query);
  return path;
}