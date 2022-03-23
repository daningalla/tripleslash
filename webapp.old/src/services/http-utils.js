export const makeUrl = (base, resource, query) => {
    let url = `${base}${resource}`;
    if (query){
        url += "?";
        let count = 0;
        Object.keys(query).forEach(k => {
            if (count > 0){
                url += "&";
            }
            const v = query[k];
            url += `${k}=${v}`;
            ++count;
        });
    }
    return url;
};