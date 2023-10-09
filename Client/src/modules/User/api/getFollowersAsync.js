import axios from "axios"
import { API_USER_URL } from "../../../constants/ApiConstants"

export const getFollowersAsync = async (userEmail, page, take) => {
    try {
        const response = await axios.get(API_USER_URL + "/followers", {
            params : {
                email : userEmail,
                page : page,
                take : take
            }
        })

        return response
    }
    catch (error) {
        console.log(error)
    }
}
