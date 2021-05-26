import history from "../../entities/search/history";

export class Redirect {
    static to(path: string) {
        history.push(path);
    }

    static toLogin() {
        this.to(`/login`);
    }

    static toUsers() {
        this.to('/users');
    }

    static toUser(id: string) {
        this.to('/users/' + id);
    }

    static toRecord(id: string) {
        this.to('/records/' + id);
    }
}