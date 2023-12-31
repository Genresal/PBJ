import axios from "axios"
import { IDENTITY_USER_URL } from "../../../constants/ApiConstants"

export const editUserAsync = async (email, userRequest) => {
    try {
        const response = await axios.put(IDENTITY_USER_URL, userRequest,
        {
            params: {
                email: email
            },
            headers: {
                'Content-Type': 'application/json'
            }
        })

        return response
    } 
    catch (error) {
        console.log(error);
    }
}