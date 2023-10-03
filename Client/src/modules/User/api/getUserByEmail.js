import axios from "axios"
import { API_USER_URL } from "../../../constants/ApiConstants"

export const getUserByEmail = async (userEmail) => {
    try {
        var response = await axios.get(API_USER_URL + "/email", {
            params: {
                email: userEmail
            }
        })
    
        return response.data;
    }
    catch (error) {
        console.log(error.response.status);
    }
    
}