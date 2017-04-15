export class Tag { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Tag {
        let tag = new Tag();
        tag.name = data.name;
        return tag;
    }
}
