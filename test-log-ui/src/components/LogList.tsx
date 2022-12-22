import { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { Badge, Button, Col, ListGroup, ListGroupItem, ListGroupItemHeading, ListGroupItemText, Row } from "reactstrap";
import { useFilterStore } from "../stores/filterStore";
import { useLogStore } from "../stores/logStore";
import { postFilterData } from "../utils/api";
import LogItems from "./LogItems";

const LogList = () => {

    const [logsCategories, increaseLogs] = useLogStore((state) => [state.logs, state.increaseLogs]);
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
        <>
            <ListGroup className="mt-2">
                {logsCategories.map((category, index) =>
                    <ListGroupItem key={index}>
                        <ListGroupItemHeading>
                            <div className="text-center">
                           Last Date: {category.lastDate} - Last Ip: {category.lastIp} - Last Message: {category.lastMessage} <Badge>{category.name}</Badge>
                           </div>
                        </ListGroupItemHeading>
                        <ListGroupItemText>
                            <LogItems logs={category.logs} />
                        </ListGroupItemText>
                    </ListGroupItem>
                )}
            </ListGroup>
            <Row className="mt-2">
                <Col>
                    <Button disabled={isLoading || !filter.initialState} block size="sm" onClick={loadMore}> Load more... </Button>
                </Col>
            </Row>
        </>
    )
}

export default LogList;