export class TokenDTO {
    constructor(
        private _token: string,
        public userName: string,
        public _tokenExpirationDate: Date,
        public user_id:string,
    ) {}

    get token() {
        if (!this._tokenExpirationDate || new Date() > this._tokenExpirationDate) {
            return null;
        }
        return this._token;
    }
}