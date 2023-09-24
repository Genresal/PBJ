import { userManager } from "../services/AuthService";

export default function Callback() {
  return (
    <>{
        userManager.signinRedirectCallback().then(() => {
            window.location.href = "http://localhost:3000"
        })
    }</>
  )
}
