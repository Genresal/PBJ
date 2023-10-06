export default function Post({post}) {

    return(
        <div>
            <strong>{post.id}</strong>
            <div>{post.content}</div>
        </div>
    );
}
