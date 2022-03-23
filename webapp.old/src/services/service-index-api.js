import { of } from "rxjs";
import { fromFetch } from "rxjs/fetch";
import { tap, switchMap, catchError } from "rxjs/operators";
import { environment } from "./environment.js";

class ServiceIndexImpl {
    constructor(environment){
        this.serviceBase = environment.serviceBase;
        this.cached = null;
    }

    fetch = () => {
        const me = this;
        const url = `${this.serviceBase}/service`;
        console.log(`Fetching ${url}`);
        return fromFetch(url).pipe(
            switchMap((response) => {
                console.log("In switchmap");
                console.log("response = " + response);
                if (response.ok){
                    return response.json();
                }
                else {
                    console.log("There was an error");
                    return of({ error: true, message: `Error ${response.status}` });
                }
            }),
            catchError(err => {
                console.log("Error = " + err);
            }), 
            tap(obj => { me.cached = obj; })
        );
    }

    getResource() {
        return this.cached
            ? of(this.cached)
            : this.fetch();
    };
}

export const serviceIndex = new ServiceIndexImpl(environment);