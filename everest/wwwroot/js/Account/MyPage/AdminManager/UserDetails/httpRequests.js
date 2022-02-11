


export const confirmRoleRequest = async (userId,role,classifications = []) => {

    const res = await fetch(`/api/users/change-role?userId=${userId}&role=${role}`, {
        method: "POST",
        headers: {
            "Content-Type" : "application/json"
        },
        body: JSON.stringify(classifications)
    });

    if (!res.ok) throw new Error("خطا أثناء الطلب حاول مجددا");

    location.reload();
    
}