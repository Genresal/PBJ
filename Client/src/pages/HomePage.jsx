// import Posts from "../modules/Post/components/Posts/Posts.jsx";
import NavMenu from "../UI/NavMenu/NavMenu.jsx";
import { useState } from "react";
import { getByUserIdAsync } from "../modules/Post/api/getByUserIdAsync.js"
import {userManager} from "../services/AuthService.js"

export default function HomePage() {

  const [clicked, setClicked] = useState(false);
  const [posts, setPosts] = useState([]);


  const getToken = () => {
    userManager.getUser().then((user) => {
      if(user) {
        console.log(user.access_token)
      }
      else {
        console.log("Ты еблан");
      }
    })
  }

  const getPosts = async () => {
    const response = await getByUserIdAsync(1, 1, 1)
  
    setPosts([...posts, ...response.data.items])
  }

  console.log(posts)

  return (
    <>
      <NavMenu/>

      <button onClick={() => userManager.signinRedirect()}>Log In</button>
      <button onClick={() => getToken()}>Get Token</button>
      <button onClick={() => {getPosts(); setClicked(true)}}>Get Posts</button>

      {clicked &&
        posts.map(post => 
            <div key={post.id}>post.content</div>
          )
      }
      
      {/* <Posts/> */}
    </>
  )
}
