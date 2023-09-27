import { useContext } from "react"
import { PagesContext } from "../PagesProvider"

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
