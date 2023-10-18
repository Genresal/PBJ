import axios from "axios"
import { API_USER_URL } from "../../../constants/ApiConstants"

export const getUsersByEmailPartAsync = async (emailPart, take) => {
    try {
        const response = axios.get(API_USER_URL + "/search", {
            params : {
                emailPart : emailPart,
                take : take
            }
        });

        return response;
    }
    catch (error) {
        console.log(error)

        return 500
    }
}