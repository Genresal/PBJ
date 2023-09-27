import { useContext } from "react"
import { PagesContext } from "../modules/Provider/PagesProvider"

export default function CallbackPage() {
  const {userManager} = useContext(PagesContext)

  return (
    <>
      {
        userManager.signinRedirectCallback().then(user => {
          window.location.href = "http://localhost:3000"
        })
      }   
    </>
  )
}
