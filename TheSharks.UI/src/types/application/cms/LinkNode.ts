export interface LinkNode {
    title: string;
    link: string;
    membersOnly: boolean;
    navBarPosition: number;
    navBarSubPosition: number;
    children: LinkNode[] | null;
}