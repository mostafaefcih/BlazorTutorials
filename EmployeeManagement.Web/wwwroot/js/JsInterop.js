function CreateAlert() {
    alert("Welcom to js from Blazor")
}
function CreatePrompte(question) {

    return prompt(question)
}
function setElementTextById(id, text) {
    document.getElementById(id).innerText = text;

}
function SetElementFocus(element) {
    element.focus()
}
function randomize(maxNumber) {
    DotNet.invokeMethodAsync('EmployeeManagement.Web', 'Randomize', maxNumber)
        .then(result => {
            setElementTextById("randomNumber", result)
        })
        ;
}