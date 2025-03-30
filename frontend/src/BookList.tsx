import { useEffect, useState } from "react";
import { Book } from "./types/Book";

function BookList() {

    const [books, setBooks] = useState<Book[]>([]);
    const [pageSize, setPageSize] = useState<number>(5);
    const [pageNum, setPageNum] = useState<number>(1);
    const [totalItems, setTotalItems] = useState<number>(0);
    const [totalPages, setTotalPages] = useState<number>(0);

    useEffect(() =>{
        const fetchBooks = async () => {
            const response = await fetch(`https://localhost:5000/api/Book?pageHowMany=${pageSize}&pageNum=${pageNum}`)
            const data = await response.json();
            setBooks(data.books);
            setTotalItems(data.totalNumBooks)
            setTotalPages(Math.ceil(data.totalNumBooks/pageSize));
        };

        fetchBooks();
    },[pageSize, pageNum]);

    return (
        <>
            <h1>Books</h1>
            <br/>
            {books.map((b) => (
            <div id="bookCard" key={b.bookId} className="card">
                <h3>{b.title}</h3>
                <div className="card-body">
                <ul className= "list_unstyled">
                <li>Author: {b.author}</li>
                <li>Publisher: {b.publisher}</li>
                <li>ISBN: {b.isbn}</li>
                <li>Classification: {b.classification}</li>
                <li>Category: {b.category}</li>
                <li>Page Count: {b.pageCount}</li>
                <li>Price: ${b.price.toFixed(2)}</li>
                </ul>
                </div>
            </div>
            ))}
        <button onClick={() => setPageNum(pageNum - 1)}>Previous</button>

        {[...Array(totalPages)].map((_, i) => (
            <button key={i+1} onClick={() => setPageNum(i + 1)}>
                {i+1}
            </button>
        ))}

        <button onClick={() => setPageNum(pageNum + 1)}>Next</button>

        <br />
        <label>
            Results per page:
            <select 
                value={pageSize} 
                onChange={(e) => setPageSize(Number(e.target.value))}>

                <option value="5">5</option>
                <option value="10">10</option>
                <option value="20">20</option>
            </select>
        </label>
        </>
    );

}

export default BookList;