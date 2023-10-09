import { useContext } from "react"
import NavMenu from "../UI/NavMenu/NavMenu"
import Posts from "../modules/Post/components/Posts/Posts"
import { PagesContext } from "../modules/Provider/PagesProvider"

export default function HomePage() {

  const {user} = useContext(PagesContext)
  
  return (
    <>
      <NavMenu/>

      <Posts user={user}/>
    </>
  )
}
