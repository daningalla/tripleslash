class Environment {
    constructor(serviceBase){
        this.serviceBase = serviceBase;
    }
}

export const environment = new Environment("https://localhost:7076");
