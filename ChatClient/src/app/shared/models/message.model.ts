export class MessageModel {
  constructor(
    public userName: string,
    public dateTime: Date,
    public messageText: string,
    public messageType: string
  ) {}
}
