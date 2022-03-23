import { environment } from "./environment";
import { fromFetch } from "rxjs/fetch";
import serviceIndex from "./service-index-api";
import { mergeMap, switchMap } from "rxjs";
import { makeUrl } from "./http-utils";

class SearchServiceImpl {
    constructor(environment){
        this.environment = environment;
    }

    getResource(term, page, size, preRelease){
        return serviceIndex
            .getResource()
            .pipe(
                mergeMap(result => {
                    const resource = result.resources.find(resource => resource.type === "SearchService");
                    const url = makeUrl(environment.serviceBase,
                        resource.id,
                        {
                            eco: "dotnet",
                            term,
                            pg: page,
                            sz: size,
                            pre: preRelease
                        });
                    return fromFetch(url);                    
                 }),
            )
        
    }
}

const searchService = new SearchServiceImpl(environment);

export default searchService;