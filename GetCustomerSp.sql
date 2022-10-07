DELIMITER && 
CREATE PROCEDURE GetCustomer (
    IN lastId INT,
    IN pageSize INT
)
LANGUAGE SQL
NOT DETERMINISTIC
READS SQL DATA
SQL SECURITY DEFINER
COMMENT 'Provides customers with padgination'
BEGIN
	SELECT cs.customer_id as Id, 
    cs.store_id as StoreId, 
    cs.first_name as FirstName, 
    cs.last_name as LastName, 
    cs.email as Email,
    concat(ad.address, ',', ad.address2, ',', district) as Address
    FROM customer cs
    INNER JOIN address ad
    ON ad.address_id = cs.address_id
	AND cs.customer_id > lastId
	ORDER BY cs.customer_id
	LIMIT pageSize;
END && DELIMITER ; 