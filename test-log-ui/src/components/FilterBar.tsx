import { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { Button, Card, CardBody, Col, Input, Label, Row } from "reactstrap";
import { useFilterStore } from "../stores/filterStore";
import { useLogStore } from "../stores/logStore";
import { postFilterData, postPersistLogs } from "../utils/api";

const FilterBar = () => {
    const [submitAction, setSubmitAction] = useState<boolean>(false);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [categoryFilter, setCategoryFilter] = useState<string>('');
    const [messageFilter, setMessageFilter] = useState<string>('');
    const [ipFilter, setIpFilter] = useState<string>('');
    const [startFilter, setStartFilter] = useState<string>('');
    const [endFilter, setEndFilter] = useState<string>('');
    const [quantityFilter, setQuantityFilter] = useState<number>(25);
    const setLogs = useLogStore((state) => state.setLogs);
    const [filter, setFilter] = useFilterStore((state) => [state.filter, state.setFilter]);


    useEffect(() => {
        setFilter({
            category: categoryFilter,
            message: messageFilter,
            ip: ipFilter,
            quantity: quantityFilter,
            page: 1,
            start: startFilter || null,
            end: endFilter || null,
            initialState: submitAction
        });
    }, [categoryFilter, messageFilter, ipFilter, startFilter, endFilter, quantityFilter, submitAction]);


    const persistLogs = async () => {
        setIsLoading(true);
        try {
            await postPersistLogs();
        } catch (err) {
            const error = err as AxiosError;
            alert(error.message);
        } finally {
            setIsLoading(false);
        }
    }

    const search = async () => {
        setSubmitAction(true);
        setIsLoading(true);
        try {
            const logs = await postFilterData(filter);
            setLogs(logs);

        } catch (err) {
            const error = err as AxiosError;
            alert(error.message);
        }
        finally {
            setIsLoading(false);
        }
    }

    return (
        <Card className="mt-2 ">
            <CardBody>
                <Row>
                    <Col lg={4}>
                        <Label for="category">
                            Category
                        </Label>
                        <Input
                            id="category"
                            name="category"
                            placeholder="Enter Category"
                            type="text"
                            onChange={(e) => setCategoryFilter(e.target.value)}
                        />
                    </Col>
                    <Col lg={4}>
                        <Label for="message">
                            Message
                        </Label>
                        <Input
                            id="message"
                            name="message"
                            placeholder="Enter Message"
                            type="text"
                            onChange={(e) => setMessageFilter(e.target.value)}
                        />
                    </Col>
                    <Col lg={4}>
                        <Label for="ip">
                            Ip
                        </Label>
                        <Input
                            id="ip"
                            name="ip"
                            placeholder="Enter Ip"
                            type="text"
                            onChange={(e) => setIpFilter(e.target.value)}
                        />
                    </Col>
                    <Col lg={4}>
                        <Label for="start">
                            Start
                        </Label>
                        <Input
                            id="start"
                            name="start"
                            type="date"
                            onChange={(e) => setStartFilter(e.target.value)}
                        >
                        </Input>
                    </Col>
                    <Col lg={4}>
                        <Label for="end">
                            End
                        </Label>
                        <Input
                            id="end"
                            name="end"
                            type="date"
                            onChange={(e) => setEndFilter(e.target.value)}
                        >
                        </Input>
                    </Col>
                    <Col lg={4}>
                        <Label for="quantity">
                            Quantity
                        </Label>
                        <Input
                            id="quantity"
                            name="select"
                            type="select"
                            onChange={(e) => setQuantityFilter(parseInt(e.target.value))}
                        >
                            <option>
                                25
                            </option>
                            <option>
                                50
                            </option>
                            <option>
                                100
                            </option>
                            <option>
                                200
                            </option>
                            <option>
                                300
                            </option>
                        </Input>
                    </Col>
                </Row>
                <Row className="mt-2">
                    <Col lg={1} >
                        <Button onClick={search} className="mt-1" disabled={isLoading} block>
                            Search Logs
                        </Button>
                    </Col>
                    <Col lg={1} >
                        <Button color="danger" className="mt-1" onClick={persistLogs} disabled={isLoading} block>
                            Persist Logs
                        </Button>
                    </Col>
                </Row>
            </CardBody>
        </Card>
    )
}


export default FilterBar;

function useLogsStore(arg0: (state: any) => any) {
    throw new Error("Function not implemented.");
}
