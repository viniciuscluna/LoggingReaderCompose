import { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { Button, Table } from "reactstrap";
import { useFilterStore } from "../stores/filterStore";
import { useLogStore } from "../stores/logStore";
import { postFilterData } from "../utils/api";

const LogList = () => {

    const [logs, increaseLogs] = useLogStore((state) => [state.logs, state.increaseLogs]);
    const [filter, increasePage] = useFilterStore((state) => [state.filter, state.increasePage]);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    useEffect(() => {
        if (filter.page > 1) {
            async function fetchCall() {
                setIsLoading(true);
                try {
                    const logs = await postFilterData(filter);
                    increaseLogs(logs);
                } catch (err) {
                    const error = err as AxiosError;
                    alert(error.message);
                } finally {
                    setIsLoading(false);
                }
            }
            fetchCall();
        }
    }, [filter.page])


    const loadMore = () => increasePage();

    return (
        <Table>
            <thead>
                <tr>
                    <th>
                        Date
                    </th>
                    <th>
                        Ip
                    </th>
                    <th>
                        Category
                    </th>
                    <th>
                        Message
                    </th>
                </tr>
            </thead>
            <tbody>
                {logs.map((log, index) =>
                    <tr key={index}>
                        <th scope="row">
                            {log.date}
                        </th>
                        <td>
                            {log.ip}
                        </td>
                        <td>
                            {log.category}
                        </td>
                        <td>
                            {log.message}
                        </td>
                    </tr>
                )}

                <tr>
                    <th colSpan={4} scope="row" className="text-center">
                        <Button disabled={isLoading || !filter.initialState} block size="sm" onClick={loadMore}> Load more... </Button>
                    </th>

                </tr>
            </tbody>
        </Table>
    )
}

export default LogList;