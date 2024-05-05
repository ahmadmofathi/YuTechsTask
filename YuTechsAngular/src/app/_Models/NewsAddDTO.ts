export class NewsAddDTO {
    constructor(
        public title: string,
        public newsContent: string,
        public image: string,
        public publicationDate: string,
        public creationDate: string,
        public authorId: string,
    ) {}
}