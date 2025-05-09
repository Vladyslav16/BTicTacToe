
let board = [["", "", ""], ["", "", ""], ["", "", ""]];
let playerSymbol = "X";
let botSymbol = "O";
let currentPlayer = "X";
let gameOver = false;
let xWins = 0;
let oWins = 0;

function makeMove(i, j) {
    if (gameOver || board[i][j] !== "") return;
    board[i][j] = currentPlayer;
    document.getElementById(`cell-${i}-${j}`).innerText = currentPlayer;

    if (checkWin(currentPlayer)) {
        document.getElementById("status").innerText = currentPlayer + " виграв!";
        if (currentPlayer === "X") xWins++; else oWins++;
        updateScore();
        gameOver = true;
        return;
    }

    if (isDraw()) {
        document.getElementById("status").innerText = "Нічия!";
        gameOver = true;
        return;
    }

    currentPlayer = currentPlayer === "X" ? "O" : "X";
    if (currentPlayer === botSymbol) botMove();
}

function botMove() {
    const difficulty = document.getElementById("difficulty").value;
    if (difficulty === "easy") {
        easyBotMove();
    } else if (difficulty === "medium") {
        mediumBotMove();
    } else {
        hardBotMove();
    }
}

function easyBotMove() {
    let empty = [];
    for (let i = 0; i < 3; i++)
        for (let j = 0; j < 3; j++)
            if (board[i][j] === "") empty.push([i, j]);
    if (empty.length === 0) return;
    let [i, j] = empty[Math.floor(Math.random() * empty.length)];
    makeMove(i, j);
}

function mediumBotMove() {
    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            if (board[i][j] === "") {
                board[i][j] = botSymbol;
                if (checkWin(botSymbol)) {
                    applyBotMove(i, j);
                    return;
                }
                board[i][j] = "";
            }
        }
    }

    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            if (board[i][j] === "") {
                board[i][j] = playerSymbol;
                if (checkWin(playerSymbol)) {
                    board[i][j] = "";
                    applyBotMove(i, j);
                    return;
                }
                board[i][j] = "";
            }
        }
    }

    easyBotMove();
}

function hardBotMove() {
    let bestScore = -Infinity;
    let move = null;

    for (let i = 0; i < 3; i++) {
        for (let j = 0; j < 3; j++) {
            if (board[i][j] === "") {
                board[i][j] = botSymbol;
                let score = minimax(board, 0, false);
                board[i][j] = "";
                if (score > bestScore) {
                    bestScore = score;
                    move = { i, j };
                }
            }
        }
    }

    if (move) makeMove(move.i, move.j);
}

function minimax(boardState, depth, isMaximizing) {
    if (checkWin(botSymbol)) return 10 - depth;
    if (checkWin(playerSymbol)) return depth - 10;
    if (isDraw()) return 0;

    if (isMaximizing) {
        let maxEval = -Infinity;
        for (let i = 0; i < 3; i++) {
            for (let j = 0; j < 3; j++) {
                if (boardState[i][j] === "") {
                    boardState[i][j] = botSymbol;
                    let eval = minimax(boardState, depth + 1, false);
                    boardState[i][j] = "";
                    maxEval = Math.max(maxEval, eval);
                }
            }
        }
        return maxEval;
    } else {
        let minEval = Infinity;
        for (let i = 0; i < 3; i++) {
            for (let j = 0; j < 3; j++) {
                if (boardState[i][j] === "") {
                    boardState[i][j] = playerSymbol;
                    let eval = minimax(boardState, depth + 1, true);
                    boardState[i][j] = "";
                    minEval = Math.min(minEval, eval);
                }
            }
        }
        return minEval;
    }
}

function resetGame() {
    board = [["", "", ""], ["", "", ""], ["", "", ""]];
    currentPlayer = playerSymbol;
    gameOver = false;
    document.getElementById("status").innerText = "";
    for (let i = 0; i < 3; i++)
        for (let j = 0; j < 3; j++)
            document.getElementById(`cell-${i}-${j}`).innerText = "";
}

function updateScore() {
    document.getElementById("score").innerText = `Рахунок — X: ${xWins} | O: ${oWins}`;
}

function changeSymbol() {
    if (board.flat().some(cell => cell !== "")) {
        alert("Неможливо змінити символ після початку гри.");
        return;
    }
    playerSymbol = playerSymbol === "X" ? "O" : "X";
    botSymbol = playerSymbol === "X" ? "O" : "X";
    currentPlayer = playerSymbol;
    document.getElementById("symbol").innerText = playerSymbol;
}

function checkWin(p) {
    for (let i = 0; i < 3; i++)
        if (board[i][0] === p && board[i][1] === p && board[i][2] === p) return true;
    for (let j = 0; j < 3; j++)
        if (board[0][j] === p && board[1][j] === p && board[2][j] === p) return true;
    if (board[0][0] === p && board[1][1] === p && board[2][2] === p) return true;
    if (board[0][2] === p && board[1][1] === p && board[2][0] === p) return true;
    return false;
}

function isDraw() {
    return board.flat().every(cell => cell !== "");
}

function applyBotMove(i, j) {
    board[i][j] = botSymbol;
    document.getElementById(`cell-${i}-${j}`).innerText = botSymbol;

    if (checkWin(botSymbol)) {
        document.getElementById("status").innerText = botSymbol + " виграв!";
        oWins++;
        updateScore();
        gameOver = true;
        return;
    }

    if (isDraw()) {
        document.getElementById("status").innerText = "Нічия!";
        gameOver = true;
        return;
    }

    currentPlayer = playerSymbol;
}
