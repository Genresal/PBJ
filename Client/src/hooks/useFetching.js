import {useState} from "react";

export const useFetching = (callback) => {

    const [isLoading, setLoading] = useState(false)

    const fetching = async () => {
        try {
            setLoading(true);
            await callback();
        }
        catch (ex) {
            console.log(ex.message)
        }
        finally {
            setLoading(false);
        }
    }

    return [fetching, isLoading];
}