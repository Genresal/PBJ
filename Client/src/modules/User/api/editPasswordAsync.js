import axios from "axios"
import { IDENTITY_USER_URL } from "../../../constants/ApiConstants"

export const editPasswordAsync = async (email, passwordRequest) => {

    try {
        const response = await axios.put(IDENTITY_USER_URL + "/password", passwordRequest, {
            params: {
                email : email
            }
        })

        return response
    }
    catch (error) {
        console.log(error)
    }
}
    