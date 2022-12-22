import { Table } from "reactstrap"
import { LogData } from "../types/logData"


type LogItemsProps = {
    logs: LogData[];
}

const LogItems = ({logs}: LogItemsProps) => {

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
        </tbody>
    </Table>
    )
}

export default LogItems;